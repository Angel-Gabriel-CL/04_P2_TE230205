namespace Ejercicio4_CargaBateria
{
    /// <summary>
    /// Clase que simula el sistema de carga gradual para una batería hasta 12.6 V.
    /// </summary>
    public class SistemaCargaBateria
    {
        // PROPIEDADES
        public double VoltajeActual { get; set; }   // Voltaje actual de la batería en Voltios (V)
        public double Incremento { get; set; }      // Incremento de voltaje por ciclo en Voltios (V)

        // Constante del límite superior de carga
        public const double VOLTAJE_LIMITE = 12.6; // Límite objetivo en Voltios (V)

        /// <summary>
        /// Constructor para inicializar el voltaje inicial y la tasa de incremento.
        /// </summary>
        public SistemaCargaBateria(double voltajeInicial, double incremento)
        {
            VoltajeActual = voltajeInicial;
            Incremento = incremento;
        }

        /// <summary>
        /// Método que ejecuta el ciclo de carga y previene bucles infinitos.
        /// </summary>
        public void EjecutarProcesoCarga()
        {
            // PREVENCIÓN DE CICLOS INFINITOS:
            // Si el incremento es 0 o negativo, la condición de parada del bucle nunca se alcanzaría.
            if (Incremento <= 0)
            {
                Console.WriteLine("\n[ERROR CRÍTICO]: El incremento debe ser un valor mayor a 0 V para evitar un ciclo infinito.");
                return;
            }

            // Si el voltaje inicial ya sobrepasa o iguala el límite objetivo
            if (VoltajeActual >= VOLTAJE_LIMITE)
            {
                VoltajeActual = VOLTAJE_LIMITE;
                Console.WriteLine($"\nLa batería ya se encuentra cargada al límite máximo de {VOLTAJE_LIMITE} V.");
                return;
            }

            int ciclo = 0;
            Console.WriteLine($"\n--- INICIANDO PROCESO DE CARGA (Inicio: {VoltajeActual:F2} V) ---");

            // Ciclo while para aumentar progresivamente el voltaje mientras sea menor a 12.6 V
            while (VoltajeActual < VOLTAJE_LIMITE)
            {
                ciclo++;
                VoltajeActual += Incremento;

                // Si el último incremento supera el límite, ajustar exactamente a 12.6 V
                if (VoltajeActual > VOLTAJE_LIMITE)
                {
                    VoltajeActual = VOLTAJE_LIMITE;
                }

                // Despliegue de cada iteración con el número de ciclo y voltaje actualizado con unidades
                Console.WriteLine($"Ciclo #{ciclo}: Voltaje = {VoltajeActual:F2} V");
            }

            Console.WriteLine($"\nProceso finalizado con éxito. Voltaje Final Ajustado: {VoltajeActual:F2} V");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("  EJERCICIO 4: CONTROL DE CARGA DE BATERÍA (12.6 V)");
            Console.WriteLine("====================================================\n");

            // Lectura del voltaje inicial
            double vInicial;
            Console.Write("Ingrese el voltaje inicial de la batería (V): ");
            while (!double.TryParse(Console.ReadLine(), out vInicial) || vInicial < 0)
            {
                Console.Write("Entrada no válida. Ingrese un voltaje inicial válido (V): ");
            }

            // Lectura del incremento por ciclo
            double incremento;
            Console.Write("Ingrese el incremento de voltaje por ciclo (V): ");
            while (!double.TryParse(Console.ReadLine(), out incremento) || incremento <= 0)
            {
                Console.Write("El incremento debe ser mayor a 0 V para evitar ciclos infinitos. Ingrese un valor válido: ");
            }

            // INSTANCIACIÓN DEL OBJETO DE LA CLASE
            SistemaCargaBateria cargador = new SistemaCargaBateria(vInicial, incremento);

            // Invocación del método principal
            cargador.EjecutarProcesoCarga();
        }
    }
}