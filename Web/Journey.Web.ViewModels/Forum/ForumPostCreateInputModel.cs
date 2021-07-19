namespace Journey.Web.ViewModels.Forum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Forum.Categories;

    public class ForumPostCreateInputModel : IMapTo<ForumPost>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
