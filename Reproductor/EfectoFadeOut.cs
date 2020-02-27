using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NAudio.Wave;

namespace Reproductor
{
    class EfectoFadeOut : ISampleProvider
    {
        //Entrada
        private ISampleProvider fuente;
        private float duracion;
        private float inicio;

        private int muestrasLeidas = 0;
        private float segundosTranscurridos = 0;

        public EfectoFadeOut(ISampleProvider fuente, float inicio, float duracion)
        {
            this.fuente = fuente;
            this.inicio = inicio;
            this.duracion = duracion;
         
        }
        public WaveFormat WaveFormat
        {
            get
            {
                return fuente.WaveFormat;
            }
        }
        public int Read(float[] buffer, int offset, int count)
        {
            int read = fuente.Read(buffer, offset, count);
            //Aplicar el efecto

            muestrasLeidas += read;
            segundosTranscurridos = (float)muestrasLeidas / (float)fuente.WaveFormat.SampleRate
                / (float)fuente.WaveFormat.Channels;
            //proceso - modificacion de los valores de buffer

            //salida - la variable buffer modificada

            if (segundosTranscurridos >= inicio)
            {
                //Determinar factor de escala
                //aplicar el efecto
                float factorEscala = ((inicio + duracion)-segundosTranscurridos) / duracion;
                //Escalamos muestras
                if ( factorEscala <= -0.1)
                {
                    factorEscala = 0;
                }
                for (int i = 0; i < read; i++)
                {
                    buffer[i + offset] *= factorEscala;
                }
            }

            //salida - la variable buffer modificada
            return read;
        }

    }
}
