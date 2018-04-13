using Core.Domains;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Web.ViewModels
{
    public class ResourceViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Initial Count")]
        public int InitCount { get; set; }

        [Required]
        [Display(Name = "Current Count")]
        public int CurrentCount { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public Facility Facility { get; set; }

        public IEnumerable<Facility> ListOfAllFacilities { get; set; }
        [Required]
        public int? FacilityId { get; set; }

        public ResourceViewModel(Resource resource)
        {
            Id = resource.Id;
            Name = resource.Name;
            Description = resource.Description;
            InitCount = resource.InitialCount;
            Comment = resource.Comment;
            CurrentCount = resource.CurrentCount;
            IsActive = resource.IsActive;
            Facility = resource.Facility;
            FacilityId = resource.FacilityId;

        }

        public ResourceViewModel()
        {

        }
    }
}