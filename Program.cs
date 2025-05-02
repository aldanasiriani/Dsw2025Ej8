using Dsw2025Ej8.Domain;

namespace Dsw2025Ej8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CuentaCorriente cuenta1 = new("1", 9000, new[] { "Maria" });
            cuenta1.comision = 0.01m;
            cuenta1.limiteDeDescubierto = 5000;

            CuentaCorriente cuenta2 = new("2", 3600, new[] { "Pedro" });
            cuenta2.comision = 0.01m;
            cuenta2.limiteDeDescubierto = 5000;

            CajaDeAhorro caja1 = new("3", 5655, new[] { "Pepe" });
            caja1.tasaDeInteres = 0.02m;

            CajaDeAhorro caja2 = new("4", 3951, new[] { "Jose" });
            caja2.tasaDeInteres = 0.02m;


            cuenta1.Depositar(1000);
            cuenta1.Retirar(2000);


            caja1.Depositar(5000);
            caja1.AplicarInteres();
            caja1.Retirar(1000);



            Console.WriteLine($"Cuenta1, numero:{cuenta1.numero} Tipo:{cuenta1.tipo} saldo: {cuenta1.saldo}");
            Console.WriteLine($"Depositar -100");
            try
            {
                cuenta1.Depositar(-100); // monto inválido
            }
            catch (MontoNoValidoException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");

            Console.WriteLine($"Cuenta2, numero:{cuenta2.numero} Tipo:{cuenta2.tipo} saldo: {cuenta2.saldo}");

            Console.WriteLine("");

            Console.WriteLine($"Caja1, numero:{caja1.numero} Tipo:{caja1.tipo} saldo: {caja1.saldo}");

            Console.WriteLine("");

            Console.WriteLine($"Caja2 Antes , numero:{caja2.numero} Tipo:{caja2.tipo} saldo: {caja2.saldo}");
            Console.WriteLine($"Retirar 999999");
            try
            {
                caja2.Retirar(999999); // va a suspenderla
            }
            catch (SaldoInsuficienteException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"Caja2 Despues , numero:{caja2.numero} Tipo:{caja2.tipo} saldo: {caja2.saldo}");

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine($"Caja2 , numero:{caja2.numero} Tipo:{caja2.tipo} saldo: {caja2.saldo}");
            Console.WriteLine($"Depositar 99");
            try
            {
                caja2.Depositar(99); // esta suspendida
            }
            catch (CuentaNoActivaException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");
            Console.WriteLine("");

            var cuentas = new List<CuentaBancaria> { cuenta1, cuenta2, caja1, caja2 };

            foreach (var cuenta in cuentas)
            {
                var resumen = new
                {
                    numero = cuenta.numero,
                    tipo = cuenta.GetType().Name,
                    saldo = cuenta.saldo
                };
                Console.WriteLine($"RESUMEN:");
                Console.WriteLine($"Número: {resumen.numero}, Tipo: {resumen.tipo}, Saldo: {resumen.saldo}");
            }


        }
    }
}
