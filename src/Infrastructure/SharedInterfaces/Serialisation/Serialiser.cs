// BudgetFirst 
// ©2016 Thomas Mühlgrabner
//
// This source code is dual-licensed under:
//   * Mozilla Public License 2.0 (MPL 2.0) 
//   * GNU General Public License v3.0 (GPLv3)
//
// ==================== Mozilla Public License 2.0 ===================
// This Source Code Form is subject to the terms of the Mozilla Public 
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
// ================= GNU General Public License v3.0 =================
// This file is part of BudgetFirst.
//
// BudgetFirst is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// BudgetFirst is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Budget First.  If not, see<http://www.gnu.org/licenses/>.
// ===================================================================

namespace BudgetFirst.SharedInterfaces.Serialisation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// Provides (data contract) serialisation and de-serialisation
    /// </summary>
    public static class Serialiser
    {
        /// <summary>
        /// Serialise an object onto a stream
        /// </summary>
        /// <param name="object">Object to serialise. Must be have <see cref="DataContractAttribute"/>.</param>
        /// <param name="targetStream">Stream to serialise into</param>
        public static void Serialise(object @object, Stream targetStream)
        {
            var dataContractSerialiser = new DataContractSerializer(@object.GetType());
            dataContractSerialiser.WriteObject(targetStream, @object);
        }

        /// <summary>
        /// De-serialise an object from a stream
        /// </summary>
        /// <typeparam name="T">Object type. Must be have <see cref="DataContractAttribute"/>.</typeparam>
        /// <param name="sourceStream">Stream to read from</param>
        /// <returns>De-serialised object</returns>
        public static T DeSerialise<T>(Stream sourceStream)
        {
            DataContractSerializer dataContractSerializer;
            dataContractSerializer = new DataContractSerializer(typeof(T));
            return (T)dataContractSerializer.ReadObject(sourceStream);
        }

        /// <summary>
        /// De-serialise an object from a stream
        /// </summary>
        /// <typeparam name="T">Object type. Must be have <see cref="DataContractAttribute"/>.</typeparam>
        /// <param name="sourceStream">Stream to read from</param>
        /// <param name="knownTypes">Additional, known types to be supported during de-serialisation.</param>
        /// <returns>De-serialised object</returns>
        public static T DeSerialise<T>(Stream sourceStream, IEnumerable<Type> knownTypes)
        {
            DataContractSerializer dataContractSerializer;
            dataContractSerializer = new DataContractSerializer(typeof(T), knownTypes);
            return (T)dataContractSerializer.ReadObject(sourceStream);
        }
    }
}