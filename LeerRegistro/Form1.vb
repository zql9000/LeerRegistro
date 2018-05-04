Imports System.IO
Imports System.Text

Public Class frmPrincipal
    'Defino la estructura
    Private Structure Registro
        Public Id As Integer
        Public Dato As Integer
    End Structure

    'Defino la variable que voy a utilizar para abrir el archivo
    Private oArchivo As FileStream
    Private reader As BinaryReader

    Private Sub frmPrincipal_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        OpenFileDialog1.FileName = "Eventos.dat"
    End Sub

    Private Sub btnExaminar_Click(sender As System.Object, e As System.EventArgs) Handles btnExaminar.Click
        'Abro el cuadro de dialogo para abrir un archivo
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Si presiono OK, muestro el nombre del archivo y lo abro
            txtPath.Text = OpenFileDialog1.FileName
            oArchivo = File.Open(txtPath.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            reader = New BinaryReader(oArchivo, Encoding.Default)
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        'Como el timer se ejecuta todo el tiempo, verifico si se puede leer el archivo
        If Not IsNothing(oArchivo) AndAlso oArchivo.CanRead Then
            LeerValoresEnArchivo()
        End If
    End Sub

    Private Sub LeerValoresEnArchivo()
        'Obtengo los datos desde el archivo que ya está abierto y lo muestro en el formulario
        Dim oDatoBoca As Registro
        Dim oDatoOjos As Registro
        Dim oDatoModos As Registro

        'Muevo el puntero de lectura al principio del archivo
        reader.BaseStream.Position = 0

        'Leo los datos int32 en el orden correcto
        Try
            oDatoBoca.Id = reader.ReadInt32()
            oDatoBoca.Dato = reader.ReadInt32()
            oDatoOjos.Id = reader.ReadInt32()
            oDatoOjos.Dato = reader.ReadInt32()
            oDatoModos.Id = reader.ReadInt32()
            oDatoModos.Dato = reader.ReadInt32()

            'Actualizo los datos en el formulario
            lblValorBoca.Text = oDatoBoca.Dato
            lblValorOjos.Text = oDatoOjos.Dato
            lblValorModos.Text = oDatoModos.Dato
        Catch ex As Exception

        End Try
    End Sub
End Class
