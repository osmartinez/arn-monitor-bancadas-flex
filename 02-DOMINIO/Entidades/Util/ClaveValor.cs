using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Util
{
    public class ClaveValor<K,V>:Notificable
    {
        public K Clave { get; set; }
        public V Valor { get; set; }

        public ClaveValor(K k, V v)
        {
            this.Clave = k;
            this.Valor = v;
        }
    }
}
