namespace ManageLogisticsRT.Data.Models;

/// <summary>
/// Дэпо 
/// </summary>
public class Department
{
    #region [Свойства]
    /// <summary>
    /// Ид
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название Дэпо
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Город в котором находится дэпо
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// Улица
    /// </summary>
    public string Street { get; set; }
    /// <summary>
    /// Дом
    /// </summary>
    public string Home { get; set; }
    /// <summary>
    /// Почтовый индекс
    /// </summary>
    public string ZipCode { get; set; }
    #endregion

    #region [Связанные модели]
    /// <summary>
    /// Пользоваетль
    /// </summary>
    public List<User> Users { get; set; }
    #endregion
}