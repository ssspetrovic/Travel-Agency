using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Model
{
    internal class Guest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float GradeComplasent { get; set; }
        public float GradeClean { get; set; }
        public int Credits { get; set; }
        public DateTime? SuperGuestExpDate{ get; set; }

        public Guest()
        {
        }

        public Guest(int id, int userId, float gradeComplasent, float gradeClean)
        {
            Id = id;
            UserId = userId;
            GradeComplasent = gradeComplasent;
            GradeClean = gradeClean;
        }

        public Guest(int id, int userId, float gradeComplasent, float gradeClean, int credits, DateTime? superGuestExpDate) : this(id, userId, gradeComplasent, gradeClean)
        {
            Credits = credits;
            SuperGuestExpDate = superGuestExpDate;
        }

        public Guest(int id, int userId, float gradeComplasent, float gradeClean, int credits) : this(id, userId, gradeComplasent, gradeClean)
        {
            Credits = credits;
        }
    }
}
