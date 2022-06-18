using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using DataLayer.Models;


namespace  PresentationLayer.ViewModels
{
    public class UserManagerViewModel 
    {
        public List<User>? User {get;set;}

    }
}