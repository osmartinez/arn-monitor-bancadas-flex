﻿using Almacenamiento.Interfaces;
using SistemaGlobal.Update.Barquillas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almacenamiento.Implementaciones
{
    public class DaoBarquilla : IDaoBarquilla
    {
        public void Ubicar(Dictionary<int,int> idOperacion, string codSeccion,string codBarquilla, string codUbicacion)
        {
            Update.UbicarBarquilla(idOperacion,codSeccion,codBarquilla, codUbicacion);
        }
    }
}
