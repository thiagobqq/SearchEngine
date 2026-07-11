namespace EtlService.Domain.Utils;

public static class StopWords
{
    private static readonly HashSet<string> Words = new(StringComparer.OrdinalIgnoreCase)
    {
        "o", "a", "os", "as", "um", "uma", "uns", "umas",
        "de", "do", "da", "dos", "das", "em", "no", "na", "nos", "nas",
        "por", "pelo", "pela", "pelos", "pelas", "para", "com", "sem",
        "sob", "sobre", "entre", "até", "após", "desde", "contra",
        "e", "ou", "mas", "porem", "porém", "todavia", "contudo", "porque",
        "porquanto", "como", "se", "que", "pois", "portanto", "nem",
        "eu", "tu", "ele", "ela", "nós", "nos", "vós", "eles", "elas",
        "me", "te", "se", "lhe", "lhes", "este", "esta", "estes", "estas",
        "isto", "esse", "essa", "esses", "essas", "isso", "aquele", "aquela",
        "aqueles", "aquelas", "aquilo", "meu", "minha", "meus", "minhas",
        "seu", "sua", "seus", "suas", "nosso", "nossa", "nossos", "nossas",
        "qual", "quais", "quem", "cujo", "cuja", "cujos", "cujas",
        "ser", "sou", "é", "e", "somos", "são", "era", "éramos", "eram",
        "foi", "fomos", "foram", "sendo", "sido", "estar", "estou", "está",
        "estamos", "estão", "estava", "estávamos", "estavam", "esteve",
        "estiveram", "estando", "estado", "ter", "tenho", "tem", "temos",
        "têm", "tinha", "tínhamos", "tinham", "teve", "tiveram", "tendo",
        "tido", "haver", "há", "houve", "havia",
        "não", "nao", "sim", "mais", "menos", "muito", "muita", "muitos",
        "muitas", "pouco", "pouca", "poucos", "poucas", "já", "ja",
        "agora", "quando", "onde", "como", "porquê", "também", "tambem",
        "assim", "aqui", "ali", "lá", "la", "mesmo", "mesma", "mesmos",
        "mesmas", "só", "so", "apenas", "outra", "outro", "outras", "outros",
        
        "the", "is", "a", "an", "in", "on", "at", "to", "for", "of",
        "and", "or", "but", "this", "that", "it", "with", "as", "by",
        "be", "are", "was", "were", "been", "being", "have", "has",
        "had", "do", "does", "did", "will", "would", "shall", "should",
        "can", "could", "may", "might", "must", "from", "up", "about",
        "into", "over", "after", "before", "between", "under", "again",
        "further", "then", "once", "here", "there", "when", "where",
        "why", "how", "all", "any", "both", "each", "few", "more",
        "most", "other", "some", "such", "no", "nor", "not", "only",
        "own", "same", "so", "than", "too", "very", "just", "also"
    };

    public static bool Contains(string token) => Words.Contains(token);
}
