using CatsExercise.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CatsExercise.ServicesTests.Comparers
{
    internal class OwnerComparer : IComparer, IComparer<Owner>
    {
        public int Compare(Owner x, Owner y)
        {
            if (x.Name == y.Name
                && x.Gender == y.Gender
                && x.Age == y.Age)
            {
                if (x.Pets == null && y.Pets == null)
                {
                    return 0;
                }
                else
                {
                    if (x.Pets.Count() == y.Pets.Count())
                    {
                        var xPets = x.Pets.ToArray();
                        var yPets = y.Pets.ToArray();
                        for (var i = 0; i < xPets.Length; i++)
                        {
                            if (xPets[i].Name != yPets[i].Name
                                || xPets[i].PetType != yPets[i].PetType)
                            {
                                return 1;
                            }
                        }
                        return 0;
                    }
                }
            }
            return 1; // only for CollectionAssert, so there is no need for -1 value
        }

        public int Compare(object x, object y)
        {
            if (x is Owner && y is Owner)
            {
                return Compare((Owner)x, (Owner)y);
            }
            return 1;
        }
    }
}
