using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kdubois1_MVC_Music.Models
{
    public class Musician : Auditable, IValidatableObject
    {
        public Musician()
        {
            this.Performances = new HashSet<Performance>();
            this.Plays = new HashSet<Plays>();
        }

        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The first name cannot be blank.")]
        [StringLength(50, ErrorMessage = "The first name cannot exceed 50 characters.")]
        public string FName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(30, ErrorMessage = "The middle name cannot exceed 50 characters.")]
        public string MName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The last name cannot be blank. Sorry, Cher.")]
        [StringLength(50, ErrorMessage = "The last name cannot exceed 50 characters.")]
        public string LName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number cannot be blank.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 Phone { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of birth cannot be blank.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Your SIN number cannot be blank.")]
        [RegularExpression("^\\d{9}$", ErrorMessage = "SIN numbers must be exactly 9 numeric digits.")]
        [StringLength(9)]
        public string SIN { get; set; }

        [Required(ErrorMessage = "Please include an instrument")]
        [Display(Name = "Principal Instrument")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an instrument.")]
        public int InstrumentID { get; set; }

        [Display(Name = "Stage Name")]
        [StringLength(100, ErrorMessage = "Stage Names must be less than 100 characters.")]
        public string StageName { get; set; }

        public virtual Instrument Instrument { get; set; }

        public ICollection<Performance> Performances { get; set; }
        public ICollection<Plays> Plays { get; set; }

        //Summary Properties
        [Display(Name = "Formal Name")]
        public string FormalName
        {
            get
            {
                return FName
                    + (string.IsNullOrEmpty(MName) ? " " :
                        (" " + (char?)MName[0] + ". ").ToUpper())
                    + LName;
            }
        }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FName + (string.IsNullOrEmpty(MName) ? " " :
                    (" " + MName + " ")) + LName;
            }
        }

        [Display(Name = "Display Name")]
        public string DisplayName
        {
            get
            {
                return (string.IsNullOrEmpty(StageName) ? FormalName : StageName);
            }
        }

        public int Age { get
            {
                return DateTime.Now.Year - DOB.Year;
            }
        }

        //IValidation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DOB > DateTime.Today)
            {
                yield return new ValidationResult("Date of Birth cannot be in the future", new[] { "DOB" });
            }
            if (DOB > DateTime.Today.AddYears(-5))
            {
                yield return new ValidationResult("Musicians must be older than 5. Sorry Mozart.", new[] { "DOB" });
            }
        }
    }
}
