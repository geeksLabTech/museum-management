
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.ViewModels {
    public class LendingTableViewModel {
        
        public SelectList? LendingStateList {get; set;}

        public List<LendingViewModel> LendingViewModels {get; set;}
        
        public string? CurrentLendingState {get; set;}

    }
}
