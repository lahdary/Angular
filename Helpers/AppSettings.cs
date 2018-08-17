namespace Angular.Helpers
{
  /// <summary>
  /// Classe de paramétrage pour les paramètres
  /// </summary>
  public class AppSettings
  {
    public string Secret { get; set; }
    /// <summary>
    /// Lien vers le webservice MSF
    /// </summary>
    public string WS_MSF_URL { get; set; }
    public string param1 { get; set; }

    public Logins param2 { get; set; }
    /// <summary>
    /// Lien vers l'IHM
    /// </summary>
    public string URL_IHM { get; set; }

  }
  public class Logins
  {
    public string param3 { get; set; }
  }
}
