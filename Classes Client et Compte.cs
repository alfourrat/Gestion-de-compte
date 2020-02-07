class Client
{
    // Attributs
    private string cin, nom, prénom;
    private int téléphone;

    // Accesseurs
    public int Téléphone
    {
        get { return téléphone; }
        set { téléphone = value; }
    }
    
    public string Prénom
    {
        get { return prénom; }
        set { prénom = value; }
    }
    
    public string Nom
    {
        get { return nom; }
        set { nom = value; }
    }
    
    public string CIN
    {
        get { return cin; }
        set { cin = value; }
    }
    
    // Constructeurs
    public Client(string cin="", string nom="", string prénom="", int téléphone=0)
    {
        this.cin = cin;
        this.nom = nom;
        this.prénom = prénom;
        this.téléphone = téléphone;
    }

    public Client(string cin, string nom, string prénom)
    {
        this.cin = cin;
        this.nom = nom;
        this.prénom = prénom;
    }

    // Méthodes
    public void Afficher()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t\tCIN: " + cin);
        Console.WriteLine("\t\tNom: " + nom);
        Console.WriteLine("\t\tPrénom: " + prénom);
        Console.WriteLine("\t\tTéléphone: " + téléphone);
        Console.ResetColor();
    }
}

class Compte
{
    // Attributs
    private double solde;
    private int code;
    private Client propriétaire;
    private static int nombrecomptes = 0;

    public int NombreComptes
    {
        get { return nombrecomptes; }
        set { nombrecomptes = value; }
    }
    
    // Accesseurs
    public Client Propriétaire
    {
        get { return propriétaire; }
        set { propriétaire = value; }
    }

    public int Code
    {
        get { return code; }
    }

    public double Solde
    {
        get { return solde; }
    }

    // Constructeurs    
    public Compte(Client propriétaire)
    {
        nombrecomptes++;
        code = nombrecomptes;
        this.propriétaire = propriétaire;
    }

    // Méthodes
    public void Créditer(double montant)
    {
        solde += montant;
        MessageInfos("Transaction");
    }

    public void Créditer(double montant, Compte compte_débité)
    {
        if (montant <= compte_débité.solde)
        {
            solde += montant;
            compte_débité.solde -= montant;
            MessageInfos("Transaction");
        }
        else
        {
            MessageInfos("Insuffisance");
        }
        
    }

    public void Débiter(double montant)
    {
        if (montant <= solde)
        {
            solde -= montant;
            MessageInfos("Transaction");
        }
        else
        {
            MessageInfos("Insuffisance");
        }
        
    }

    public void Débiter(double montant, Compte compte_crédité)
    {
        if (montant <= solde)
        {
            solde -= montant;
            compte_crédité.solde += montant;
            MessageInfos("Transaction");
        }
        else
        {
            MessageInfos("Insuffisance");
        }
        
        
    }

    private void MessageInfos(string sujet)
    {
        switch (sujet)
        {
            case "Transaction":
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Transactions effectuéees avec succés.");
                Console.ResetColor();
                break;
            case "Insuffisance": 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Transactions non effectuéees. Solde insuffisant !");
                Console.ResetColor();
                break;
            default: 
                ArgumentException MotCléFaux = new ArgumentException();
                Console.WriteLine(MotCléFaux.Message);
                break;
        }
        
    }

    public void Afficher()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\tNuméro de compte: " + code);
        Console.WriteLine("\tSolde du compte: " + solde);
        Console.WriteLine("\tPropriétaire du compte:");
        propriétaire.Afficher();
        Console.ResetColor();
    }

    public static void AfficherNombreComptes()
    {
        Console.WriteLine("Le nombre de comptes créés est: " + nombrecomptes);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Gestion des Comptes des Clients";
        start:
        Console.Clear();
        Action<string> msg = s => Console.Write(s);
        Action<string> msgl = s => Console.WriteLine(s);
        msgl("[ Gestion des Comptes des Clients ]\n");
    //--------------------------------------------------------------------------------------------
        Compte[] account = new Compte[2];
        account[0] = new Compte(new Client());
        account[1] = new Compte(new Client());

        msgl("Compte 1:");
        msg("Donner Le CIN: "); account[0].Propriétaire.CIN = Console.ReadLine();
        msg("Donner Le Nom: "); account[0].Propriétaire.Nom = Console.ReadLine();
        msg("Donner Le Prénom: "); account[0].Propriétaire.Prénom = Console.ReadLine();
        msg("Donner Le numéro de téléphone: "); account[0].Propriétaire.Téléphone = int.Parse(Console.ReadLine());
        account[0].Afficher();
        msg("Donner le montant à déposer: "); account[0].Créditer(double.Parse(Console.ReadLine()));
        account[0].Afficher();
        msg("Donner le montant à retirer: "); account[0].Débiter(double.Parse(Console.ReadLine()));
        account[0].Afficher();

        msgl("\nCompte 2:");
        msg("Donner Le CIN: "); account[1].Propriétaire.CIN = Console.ReadLine();
        msg("Donner Le Nom: "); account[1].Propriétaire.Nom = Console.ReadLine();
        msg("Donner Le Prénom: "); account[1].Propriétaire.Prénom = Console.ReadLine();
        msg("Donner Le numéro de téléphone: "); account[1].Propriétaire.Téléphone = int.Parse(Console.ReadLine());
        account[1].Afficher();
        msgl("Créditer le compte 2 à partir du compte 1:");
        msg("Donner le montant à déposer: "); account[1].Créditer(double.Parse(Console.ReadLine()),account[0]);
        msgl("Débiter le compte 1 et créditer le commpte 2:");
        msg("Donner le montant à retirer: "); account[0].Débiter(double.Parse(Console.ReadLine()), account[1]);
        msgl("");
        account[0].Afficher();
        msgl("");
        account[1].Afficher();
        msgl("");
        Compte.AfficherNombreComptes();
    //--------------------------------------------------------------------------------------------
        Console.ReadKey();
        goto start;
    }
}