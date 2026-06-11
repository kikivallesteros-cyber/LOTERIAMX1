namespace LOTERIAMX1.Domain;

/// <summary>
/// Representa una carta de la lotería mexicana.
/// </summary>
public class Carta
{
    /// <summary>
    /// Número único de la carta (1-54).
    /// </summary>
    public int Numero { get; set; }

    /// <summary>
    /// Nombre descriptivo de la carta (ej: "El Gallo", "La Dama").
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Frase tradicional asociada a la carta.
    /// </summary>
    public string Frase { get; set; }

    public Carta(int numero, string nombre, string frase)
    {
        Numero = numero;
        Nombre = nombre;
        Frase = frase;
    }

    public override string ToString() => $"{Numero}. {Nombre}";
}
