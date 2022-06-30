using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.ViewModels {
    public class CreateRestaurationViewModel
    {

        public int ArtworkId { get; set; }
        public string TypeRestauration { get; set; }

    }
}