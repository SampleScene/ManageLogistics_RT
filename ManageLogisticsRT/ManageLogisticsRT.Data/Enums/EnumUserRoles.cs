namespace ManageLogisticsRT.Data.Enums;

/// <summary>
/// Перечисление ролей работников
/// </summary>
public enum EnumUserRoles
{
    /// <summary>
    /// Пользователь
    /// </summary>
    Customer = 0,
    /// <summary>
    /// Водитель
    /// </summary>
    Driver = 1,
    /// <summary>
    /// Логист
    /// </summary>
    Logistician = 2,
    /// <summary>
    /// Администратор филиала
    /// </summary>
    Admin = 3,
    /// <summary>
    /// Глобальный администратор/Директор
    /// </summary>
    GlobalAdmin = 4,
}