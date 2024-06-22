namespace RestauranteAtomo.model
{
  internal class Cliente
  {
    static int ultimo_id = 1;

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

    public Cliente(int id)
    {
        this.id = id;
    }
    #endregion

    #region metodos

    public override string ToString()
    {
      return "Código: " + this.id + "\nNome: " + this.nome + "\nContato: " + this.contato;
    }
    #endregion

    public override bool Equals(Object ob)
    {
      Cliente cliente = (Cliente) ob;
      return this.id == cliente.id;
    }
  }
}
