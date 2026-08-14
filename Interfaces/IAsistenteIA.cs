﻿using AsistenteOpenAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistenteOpenAI.Interfaces
{
    public interface IAsistenteIA
    {
        public Task<RespuestaIA> PreguntarAsync(PreguntaIA pregunta);

    }
}