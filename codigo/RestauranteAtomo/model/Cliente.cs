namespace RestauranteAtomo.model
{
  internal class Cliente
  {
    static int ultimo_id = 0;

    #region atributos
    private string nome;
    private string contato;
    private int id;

    public Cliente(string nome, string contato)
    {
      this.nome = nome;
      this.contato = contato;
      this.id = ultimo_id++;
    }
    #endregion

    #region propriedades
    public string Nome
    {
      get { return nome; }
      set { nome = value; }
    }

    public string Contato
    {
      get { return contato; }
      set { contato = value; }
    }

    #endregion

    #region metodos

    public override string ToString()
    {
      return "Nome: " + this.nome + "; Contato: " + this.contato;
    }
    #endregion

    public override bool Equals(Object ob)
    {
      Cliente cliente = (Cliente) ob;
      return this.id == cliente.id;
    }
  }
}
