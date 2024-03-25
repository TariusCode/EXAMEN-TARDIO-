using System;
using System.Collections.Generic;

class Paciente
{
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string TipoSangre { get; set; }
    public string Direccion { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Cedula { get; set; }

    public void ImprimirDatos()
    {
        Console.WriteLine("=====================================================================================================================================================");
        Console.WriteLine($"Cédula: {Cedula} Nombre: {Nombre} Teléfono: {Telefono} Tipo de sangre: {TipoSangre} Dirección: {Direccion} Fecha de Nacimiento: {FechaNacimiento}");
        Console.WriteLine("=====================================================================================================================================================");
    }
}

class Medicamento
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int Cantidad { get; set; }

    public void ImprimirDatos()
    {
        Console.WriteLine("========================================================");
        Console.WriteLine($"Código: {Codigo} Nombre: {Nombre} Cantidad: {Cantidad}");
        Console.WriteLine("========================================================");
    }
}

class TratamientoMedico
{
    public Paciente Paciente { get; set; }
    public List<Medicamento> Medicamentos { get; set; }

    public TratamientoMedico()
    {
        Medicamentos = new List<Medicamento>();
    }

    public void ImprimirDatos()
    {
        Paciente.ImprimirDatos();
        foreach (var medicamento in Medicamentos)
        {
            medicamento.ImprimirDatos();
        }
    }
}

class Program
{
    static List<Paciente> pacientes = new List<Paciente>();
    static List<Medicamento> catalogoMedicamentos = new List<Medicamento>();
    static List<TratamientoMedico> tratamientos = new List<TratamientoMedico>();

    static void Main(string[] args)
    {
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("=== Menú Principal ===");
            Console.WriteLine("1- Agregar paciente");
            Console.WriteLine("2- Agregar medicamento al catálogo");
            Console.WriteLine("3- Asignar tratamiento médico a un paciente");
            Console.WriteLine("4- Consultas");
            Console.WriteLine("5- Salir");

            Console.Write("Ingrese la opción deseada: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarPaciente();
                    break;
                case "2":
                    AgregarMedicamentoCatalogo();
                    break;
                case "3":
                    AsignarTratamientoMedico();
                    break;
                case "4":
                    Consultas();
                    break;
                case "5":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, ingrese un número del 1 al 5.");
                    break;
            }
        }
    }

    static void AgregarPaciente()
    {
        Console.WriteLine("=== Agregar Paciente ===");
        Paciente paciente = new Paciente();
        Console.Write("Nombre: ");
        paciente.Nombre = Console.ReadLine();
        Console.Write("Teléfono: ");
        paciente.Telefono = Console.ReadLine();
        Console.Write("Tipo de sangre: ");
        paciente.TipoSangre = Console.ReadLine();
        Console.Write("Dirección: ");
        paciente.Direccion = Console.ReadLine();
        Console.Write("Fecha de Nacimiento (dd/mm/yyyy): ");
        paciente.FechaNacimiento = DateTime.Parse(Console.ReadLine());
        Console.Write("Cédula: ");
        paciente.Cedula = Console.ReadLine();

        pacientes.Add(paciente);
        Console.WriteLine("Paciente agregado exitosamente.");
    }

    static void AgregarMedicamentoCatalogo()
    {
        Console.WriteLine("=== Agregar Medicamento al Catálogo ===");
        Medicamento medicamento = new Medicamento();
        Console.Write("Código del medicamento: ");
        medicamento.Codigo = Console.ReadLine();
        Console.Write("Nombre del medicamento: ");
        medicamento.Nombre = Console.ReadLine();
        Console.Write("Cantidad: ");
        medicamento.Cantidad = int.Parse(Console.ReadLine());

        catalogoMedicamentos.Add(medicamento);
        Console.WriteLine("Medicamento agregado al catálogo exitosamente.");
    }

    static void AsignarTratamientoMedico()
    {
        Console.WriteLine("=== Asignar Tratamiento Médico a un Paciente ===");
        Console.Write("Ingrese la cédula del paciente: ");
        string cedula = Console.ReadLine();
        Paciente paciente = pacientes.Find(p => p.Cedula == cedula);

        if (paciente == null)
        {
            Console.WriteLine("Paciente no encontrado.");
            return;
        }

        TratamientoMedico tratamiento = new TratamientoMedico();
        tratamiento.Paciente = paciente;

        Console.WriteLine("Medicamentos disponibles:");
        foreach (var medicamento in catalogoMedicamentos)
        {
            Console.WriteLine($"- {medicamento.Nombre}");
        }

        for (int i = 0; i < 3; i++)
        {
            Console.Write($"Ingrese el nombre del medicamento {i + 1}: ");
            string nombreMedicamento = Console.ReadLine();
            Medicamento medicamento = catalogoMedicamentos.Find(m => m.Nombre == nombreMedicamento);

            if (medicamento != null && medicamento.Cantidad > 0)
            {
                tratamiento.Medicamentos.Add(medicamento);
                medicamento.Cantidad--;
            }
            else
            {
                Console.WriteLine("Medicamento no encontrado en el catálogo o no hay suficiente cantidad disponible.");
                i--;
            }
        }

        tratamientos.Add(tratamiento);
        Console.WriteLine("Tratamiento asignado exitosamente.");
    }

    static void Consultas()
    {
        Console.WriteLine("=== Consultas ===");
        Console.WriteLine($"Cantidad total de pacientes registrados: {pacientes.Count}");

        Console.WriteLine("Reporte de todos los medicamentos recetados:");
        HashSet<string> medicamentosRecetados = new HashSet<string>();
        foreach (var tratamiento in tratamientos)
        {
            foreach (var medicamento in tratamiento.Medicamentos)
            {
                medicamentosRecetados.Add(medicamento.Nombre);
            }
        }
        foreach (var medicamento in medicamentosRecetados)
        {
            Console.WriteLine($"- {medicamento}");
        }

        Console.WriteLine("Reporte de cantidad de pacientes agrupados por edades:");
        int edad0a10 = 0, edad11a30 = 0, edad31a50 = 0, edadMayor51 = 0;
        DateTime fechaActual = DateTime.Now;
        foreach (var paciente in pacientes)
        {
            int edad = fechaActual.Year - paciente.FechaNacimiento.Year;
            if (fechaActual < paciente.FechaNacimiento.AddYears(edad)) edad--;
            if (edad <= 10)
                edad0a10++;
            else if (edad <= 30)
                edad11a30++;
            else if (edad <= 50)
                edad31a50++;
            else
                edadMayor51++;
        }
        Console.WriteLine($"- 0-10 años: {edad0a10}");
        Console.WriteLine($"- 11-30 años: {edad11a30}");
        Console.WriteLine($"- 31-50 años: {edad31a50}");
        Console.WriteLine($"- Mayores de 51 años: {edadMayor51}");

        Console.WriteLine("Reporte Pacientes y consultas ordenado por nombre:");
        pacientes.Sort((p1, p2) => p1.Nombre.CompareTo(p2.Nombre));
        foreach (var paciente in pacientes)
        {
            paciente.ImprimirDatos();
        }
    }

}

