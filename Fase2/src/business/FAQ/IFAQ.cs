namespace business {

    public interface IFAQ {

        int AddFAQ(string pergunta, string resposta);
        void RemoveFAQ(int idFAQ);

        ISet<FAQ> GetFAQ();

    }

}