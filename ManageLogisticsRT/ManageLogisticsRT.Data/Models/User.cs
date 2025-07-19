using ManageLogisticsRT.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
    public string FirstName { get; set; }
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string MidleName { get; set; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Дата рождения пользователя
    /// </summary>
    public DateOnly BirthDay { get; set; }
    /// <summary>
    /// Баланс пользователя
    /// </summary>
    public long Balance { get; set;}
    /// <summary>
    /// Дэпо на котором работает собтрудник
    /// </summary>
    public int? DepartmentId { get; set; }
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public EnumUserRoles UserRole {  get; set; }
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