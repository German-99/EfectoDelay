﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace Reproductor
{
    class EfectoVolumen : ISampleProvider
    {

        private ISampleProvider fuente; // entrada
        private float volumen;

        public EfectoVolumen(ISampleProvider fuente)
        {
            this.fuente = fuente;
            volumen = 1.0f;
        }

        public float Volumen
        {
            get
            {
                return volumen;
            }
            set
            {
                if (value < 0)
                {
                    volumen = 0;
                } else if (value > 1)
                {
                    volumen = 1;
                }
                else
                {
                    volumen = value;
                }
            }
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
            //procesamiento
            for(int i=0; i < read; i++)
            {
                buffer[i + offset] *= volumen;
            }
            //La variable buffer modificada es la salida
            return read;
        }
    }
}
