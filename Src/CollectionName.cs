using System;

namespace Slackers.Services.Repository
{
    public sealed class CollectionName : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionName"/> class.
        /// </summary>
        /// <param name="name">Name of collection</param>
        public CollectionName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the Name of Collection.
        /// </summary>
        public string Name { get; private set; }

    }
}