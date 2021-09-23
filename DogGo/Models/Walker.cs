using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NeighborhoodId { get; set; }
        public string NeighborhoodName { get; set; }
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public int WalkDuration { get; set; }

        //public int TotalWalkTime
        //{
        //    set
        //    {
        //        foreach (walk in WalkDuration)
        //        {
        //            TotalWalkTime = WalkDuration + WalkDuration;
        //        }
        //    }
        //}
  
    }
}
