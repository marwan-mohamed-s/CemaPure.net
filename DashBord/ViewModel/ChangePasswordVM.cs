using System.ComponentModel.DataAnnotations;

namespace DashBourd.ViewModel
{
    public class ChangePasswordVM
    {
        [Required, MinLength(6), DataType(DataType.Password)]
        public string NewPassword { get; set; } = String.Empty;

        [Required, DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = String.Empty;
        public String ApplicationUserId { get; set; } = String.Empty;

    }
}
