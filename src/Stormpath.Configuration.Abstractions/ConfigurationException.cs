// <copyright file="ConfigurationException.cs" company="Stormpath, Inc.">
// Copyright (c) 2016 Stormpath, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;

namespace Stormpath.Configuration.Abstractions
{
    /// <summary>
    /// Represents a configuration error.
    /// </summary>
    public class ConfigurationException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="ConfigurationException"/>.
        /// </summary>
        public ConfigurationException()
            : this(null, null)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ConfigurationException"/> with the given message.
        /// </summary>
        /// <param name="message"></param>
        public ConfigurationException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// /// Creates a new instance of <see cref="ConfigurationException"/> with the given message and inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
