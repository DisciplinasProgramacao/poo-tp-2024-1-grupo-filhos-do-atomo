namespace RestauranteAtomo.model
{
  internal class Cliente
  {
    #region atributos
    private string nome;
    private string contato;
    private Requisicao requisicao;

    public Cliente(string nome, string contato)
    {
      this.nome = nome;
      this.contato = contato;
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
      get { return contato;}
      set { contato = value; }
    }

    public Requisicao Requisicao
    {
      get { return requisicao;}
    }
    #endregion

    #region metodos
    
    /// <summary>
    /// cria uma nova requisicao com a quantidade de pessoas
    /// especificada
    /// </summary>
    /// <param name="quantidadePessoas"></param>
    public Requisicao fazerRequisicao(int quantidadePessoas)
    {
      requisicao = new Requisicao(quantidadePessoas);
      return requisicao;
    }    
    
    public override string ToString(){  
      return "Nome: " + this.nome + "; Contato: " + this.contato + "; " + (requisicao != null ? requisicao.ToString() + ": " : "Sem reserva pendente");
    }
    #endregion
  }
}
