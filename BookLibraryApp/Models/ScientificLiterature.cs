using BookLibraryApp.Interfaces;

namespace BookLibraryApp.Models;

public class ScientificLiterature : Book, IPrintable
{
    // атрибут из варианта :contentReference[oaicite:6]{index=6}
    public string FieldOfScience { get; set; } = "";

    // + доп. атрибуты
    public bool HasPeerReview { get; set; }
    public string doi { get; set; } = "";
    public int Citations { get; set; }

    public ScientificLiterature() { }

    public ScientificLiterature(string title, string author, int year, string isbn, int pages, decimal price,
        string fieldOfScience, bool hasPeerReview, string doi, int citations)
        : base(title, author, year, isbn, pages, price)
    {
        FieldOfScience = fieldOfScience;
        HasPeerReview = hasPeerReview;
        this.doi = doi;
        Citations = citations;
    }

    public override string GetInfo() =>
        base.GetInfo() + $", Область: {FieldOfScience}, DOI: {doi}, Цитирования: {Citations}, PeerReview: {HasPeerReview}";

    public string PrintLabel() => $"[Научн.] {Title} / {FieldOfScience}";
}
