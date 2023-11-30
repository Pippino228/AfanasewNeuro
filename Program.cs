using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace
    AfanasewNeuron
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Кол-во долларов");
            decimal input = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Стоимость рубля в долларах");
            decimal result = Convert.ToDecimal(Console.ReadLine());
            Neuron ner = new Neuron();
            int pop = 0;
            do
            {
                pop++;
                ner.Train(input, result);
                if (pop % 10000 == 0)
                {
                    Console.WriteLine("Кол-во попыток");
                    Console.WriteLine(pop);
                    Console.WriteLine("Последняя ошибка");
                    Console.WriteLine(ner.Lasterror);
                }
            } while (ner.Lasterror > ner.Smoothing || ner.Lasterror < -ner.Smoothing);
            Console.WriteLine($"{5}$ будет: {ner.ProcessInputData(5)} рублей");
            Console.WriteLine($"{10000} рублей будет: {ner.RestoreInputData(10000)}$");
            Console.ReadKey();
        }
    }
}
