namespace DevSchool.Models
{
    public class StudentInputModel
    {
        protected StudentInputModel() { }

        public StudentInputModel(string fullName, DateTime birthDate, string schoolClass)
        {
            this.FullName = fullName;
            this.BirthDate = birthDate;
            this.SchoolClass = schoolClass;
            this.IsActive = true;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public String SchoolClass { get; private set; }
        public Boolean IsActive { get; private set; }
    }
}
