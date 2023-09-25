using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [Display(Name = "Movie name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Movie Poster URL is required")]
        [Display(Name = "Movie Poster URL")]
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Movie Price in USD")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Movie Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "Movie End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Movie category is required")]
        [Display(Name = "Select a category")]
        public MovieCategory MovieCategory { get; set; }

        // Relationships
        [Required(ErrorMessage = "Movie actors is required")]
        [Display(Name = "Select actor(s)")]
        public List<int> ActorIds { get; set; }
        [Required(ErrorMessage = "Movie cinema is required")]
        [Display(Name = "Select cinema")]
        public int CinemaId { get; set; }
        [Required(ErrorMessage = "Movie Director is required")]
        [Display(Name = "Select Director")]
        public int DirectorId { get; set; }

    }

}
