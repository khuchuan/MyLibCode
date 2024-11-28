Module Module1
    Sub Main()
        Try
            Dim a As Integer = 10
            Dim b As Integer = 0
            ' Lỗi chia cho 0 bằng phép chia nguyên (\)
            Dim result As Integer = a \ b
        Catch ex As DivideByZeroException
            Console.WriteLine("Lỗi: Không thể chia cho 0.")
        Catch ex As Exception
            Console.WriteLine("Một lỗi không xác định đã xảy ra: " & ex.Message)
        Finally
            Console.WriteLine("Hoàn thành xử lý lỗi.")
        End Try

        Console.ReadLine()
    End Sub
End Module
