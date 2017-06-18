using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressApp.ViewModel.Helpers
{
    public class OrderedDictionary<TKey, TValue>
    {
        #region Atributos Privados

        //Claves y valores
        private Dictionary<TKey, TValue> _dictionary;

        //Claves agregadas al diccionario
        private List<TKey> _dictionaryKeys;

        #endregion

        #region Propiedades Publicas
        //se optiene una clave especifica
        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set
            {
                _dictionary[key] = value;
                _dictionaryKeys.Remove(key);
                _dictionaryKeys.Add(key);
            }
        }

        // Gets el numero de items.
        public int Count { get { return _dictionaryKeys.Count; } }

        // Gets el el ultimo valor agregado
        public TValue Last
        {
            get
            {
                TValue value;
                try
                {
                    _dictionary.TryGetValue(LastKey, out value);
                }
                catch (Exception)
                {
                    value = default(TValue);
                }
                return value;
            }
        }
        //Gets el ultimo clave
        public TKey LastKey { get { return _dictionaryKeys[_dictionaryKeys.Count - 1]; } }

        //Gets de los valores del diccionario
        public Dictionary<TKey, TValue>.ValueCollection Values { get { return _dictionary.Values; } }

        #endregion

        #region Contructor
        //Inicializacionde la instancia
        public OrderedDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
            _dictionaryKeys = new List<TKey>();
        }
        public OrderedDictionary(OrderedDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new Dictionary<TKey, TValue>(dictionary._dictionary);
            _dictionaryKeys = new List<TKey>(dictionary._dictionaryKeys);
        }
        #endregion

        #region Metodos Publicos
        //eliminamos una clave especifica del diccionario
        public void Remove(TKey key)
        {
            _dictionaryKeys.Remove(key);
            try
            {
                _dictionary.Remove(key);
            }
            catch (Exception)
            {
                Console.WriteLine("No se encontro clave en el diccionario");
            }
        }
        //determina si se contiene a una clave especifica
        public bool ContainsKey(TKey key)
        {
            return _dictionaryKeys.Contains(key);
        }
        #endregion

    }
}
