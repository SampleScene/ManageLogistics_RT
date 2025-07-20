using Microsoft.AspNetCore.Identity;

namespace ManageLogisticsRT.Data.Models;

/// <summary>
/// Модель пользователя 
/// </summary>
public class User : IdentityUser
{
    #region [Свойства]
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = null!;
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = null!;
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string MidleName { get; set; } = null!;
    /// <summary>
    /// Баланс пользователя
    /// </summary>
    public int? DepartmentId { get; set; }
    /// <summary>
    /// Дата регистрации пльзователя
    /// </summary>
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    #endregion
    
    #region [Связанные модели]
    /// <summary>
    /// Дэпо
    /// </summary>
    public Department? Department { get; set; }  
    #endregion
}