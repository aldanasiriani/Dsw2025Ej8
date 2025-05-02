using System;

namespace Dsw2025Ej8.Domain;

public class MontoNoValidoException : Exception
{
    public MontoNoValidoException() : base("El monto ingresado no es válido para la operación solicitada") { }
}

public class CuentaNoActivaException : Exception
{
    public CuentaNoActivaException() : base(" No se puede operar con la cuenta esta Inactiva ") { }
}

public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException() : base("La cuenta no cuenta con saldo para la operación solicitada. Fue suspendida.") { }
}

public class CuentaBancaria
{
    // público para leer, privado para escribir

    public TipoCuenta tipo { get; } //
    public string numero { get; } //
    public decimal saldo { get; set; } //
    public Estado estado { get; set; } //
    public decimal tasaDeInteres { get; set; } //
    public decimal limiteDeDescubierto { get; set; } //
    public decimal comision { get; set; } //
    public string[] titulares { get; } //

    public CuentaBancaria(string numero, decimal saldo, TipoCuenta tipo, string[] titulares)
    {
        this.numero = numero;
        this.saldo = saldo;
        this.tipo = tipo;
        this.estado = Estado.Activa;
        this.titulares = titulares;
    }

    protected void VerificarMontoValido(decimal monto)
    {
        if (monto <= 0)
            throw new MontoNoValidoException();
    }

    protected void VerificarCuentaActiva()
    {
        if (estado != Estado.Activa)
            throw new CuentaNoActivaException();
    }

}

public class CajaDeAhorro : CuentaBancaria
{
    public CajaDeAhorro(string numero, decimal saldo, string[] titulares)
        : base(numero, saldo, TipoCuenta.CajaDeAhorro, titulares)
    {
    }

    public void Depositar(decimal monto)
    {
        VerificarCuentaActiva();
        VerificarMontoValido(monto);
        saldo += monto;
    }
    public void Retirar(decimal monto)
    {
        VerificarCuentaActiva();
        VerificarMontoValido(monto);

        if (saldo < monto)
        {
            estado = Estado.Suspendida;
            throw new SaldoInsuficienteException();
        }

        saldo -= monto;
    }

    public void AplicarInteres()
    {
        VerificarCuentaActiva();
        saldo += saldo * tasaDeInteres;
    }
}


public class CuentaCorriente : CuentaBancaria
{
    public CuentaCorriente(string numero, decimal saldo, string[] titulares) : base(numero, saldo, TipoCuenta.CuentaCorriente, titulares)
    {
    }

    public void Depositar(decimal monto)
    {
        VerificarCuentaActiva();
        VerificarMontoValido(monto);
        saldo += monto - (monto * comision);
    }

    public void Retirar(decimal monto)
    {
        VerificarCuentaActiva();
        VerificarMontoValido(monto);

        if (saldo - monto >= -limiteDeDescubierto)
        {
            saldo -= monto;
        }
        if (saldo < 0)
        {
            estado = Estado.Suspendida;
            throw new SaldoInsuficienteException();
        }
    }

}