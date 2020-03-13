using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @author: Victor Noe Hernández
 * @version: 1.0.0
 * @description: class for NonlinearDataStructure
 */

namespace CustomGenerics.Interfaces {
    public abstract class DataStructure<T> {
        // Class methods
        protected abstract void InsertValue(T value, Comparison<T> comparison);
        protected abstract T DeleteValue(T value, Comparison<T> comparison);

    }
}