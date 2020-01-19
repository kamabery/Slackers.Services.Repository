using System;
using Slackers.Services.Repository;

namespace Model
{
    [CollectionName("Geography")]
    public class Geography: IEntity
    {
        public Geography()
        {
            Id = Guid.NewGuid();
        }

        public Geography(string name, string[] dangers)
        {
            Id = Guid.NewGuid();
            Name = name;
            Dangers = dangers;
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string[] Dangers { get; set; }

    }
}