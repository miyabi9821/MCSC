'MCBans�̃f�[�^�擾�������L�q����
'���ӎ����P�F�\�[�X�̔z�z���ɂ͔������ƁiMCBans�̍U���Ɏg�p����Ȃ��悤�Ɂj
'���ӎ����Q�Fonline-mode=true�̎��̂�MCBans�T�[�o�ɖ₢���킹��ioffline���ƃ��[�U�[�����̂�����̂��߁j

Module modMCBans
    Private Function pfShuffleAPIServer() As String()
        '�T�[�o���̔z��
        Dim strMCBansAPISV As String() = {"api01.cluster.mcbans.com", "api02.cluster.mcbans.com", "api03.cluster.mcbans.com", "api.mcbans.com"}

        'Fisher-Yates�A���S���Y���ŃV���b�t������ 
        Dim rng As New System.Random()
        Dim n As Integer = strMCBansAPISV.Length
        While n > 1
            n -= 1
            Dim k As Integer = rng.Next(n + 1)
            Dim tmp As String = strMCBansAPISV(k)
            strMCBansAPISV(k) = strMCBansAPISV(n)
            strMCBansAPISV(n) = tmp
        End While

        Return strMCBansAPISV
    End Function

    'MCBans��API�𗘗p���A���[�U�̐ڑ��ۂ𔻒肷��
    '�߂�l�͈ȉ��̒ʂ�
    '-1�F�s���ȃG���[
    '0�F�擾����
    '1�Fserver.properties����online-mode�̒l���擾�o���Ȃ�����
    '2�Fserver.properties��online-mode��false������
    '3�FMCBans�T�[�o���琳�����f�[�^����M�ł��Ȃ�����
    '4�F�₢���킹�ɕK�v�ȃN���C�A���gIP�A�h���X���s��
    Public Function gfGetUserStat(ByRef stat As Boolean, ByVal APIKey As String, ByVal UID As String, ByVal IP As String, ByRef reason As String) As Integer
        Try
            Dim strOnline As String = String.Empty
            If gfGetServerPropValue("online-mode", strOnline) = False Then
                'server.properties����online-mode�̒l���擾�o���Ȃ�������I��
                stat = True
                Return 1
            End If
            If CBool(strOnline) = False Then
                'server.properties����online-mode�̒l��false(�I�t���C��)��������I��
                stat = True
                Return 2
            End If
            Dim IPAddrTemp As New System.Net.IPAddress(0)
            If System.Net.IPAddress.TryParse(IP, IPAddrTemp) = False Then
                '���O����擾����IP�A�h���X���������Ȃ���ΏI��
                stat = True
                Return 4
            End If

            'API�T�[�o���V���b�t�����Ď擾����
            Dim strMCBansAPISV As String() = pfShuffleAPIServer()

            'API�T�[�o���̑䐔�Ŏ擾�o����܂Ń��[�v
            For i As Integer = 0 To strMCBansAPISV.Length - 1
                Dim wreqIP As System.Net.HttpWebRequest = _
                        CType(System.Net.HttpWebRequest.Create("http://" & strMCBansAPISV(i) & "/v2/" & APIKey & "/login/" _
                                                                & UID & "/" & IP),  _
                                                                System.Net.HttpWebRequest)
                wreqIP.UserAgent = "MCSC/" & GSTR_APP_VERSION
                wreqIP.Proxy = Nothing 'Proxy���g�p���Ȃ�
                wreqIP.Timeout = 3000 '3�b�Ń^�C���A�E�g

                '���N�G�X�g���s
                Try
                    Dim webres As System.Net.HttpWebResponse = _
                        CType(wreqIP.GetResponse(), System.Net.HttpWebResponse)
                    Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
                    Dim st As System.IO.Stream = webres.GetResponseStream()
                    Dim sr As New System.IO.StreamReader(st, enc)

                    '���X�|���X�e�L�X�g�i�[
                    Dim strTmp As String = sr.ReadToEnd()

                    sr.Close()
                    st.Close()

                    '���X�|���X���烆�[�U�[�̏�Ԃ𔻒�
                    If strTmp <> String.Empty Then
                        Dim strRes() As String = strTmp.Split(";")
                        If strRes.Length = 6 Then '�����M
                            If strRes(0) = "l" OrElse strRes(0) = "g" OrElse strRes(0) = "t" OrElse strRes(0) = "s" OrElse strRes(0) = "i" Then
                                '�v���C���[�̏�Ԃ�l,g,t,s,i�̏ꍇ�ڑ�����
                                stat = False
                                reason = strRes(1) '�ڑ����ۂ̗��R
                                Return 0
                            ElseIf Settings.Instance.MCBansMinRep > CDbl(strRes(2)) Then
                                '�v���C���[�̕]���l��臒l�������ꍇ�ڑ�����
                                stat = False
                                reason = "Reputation too low!"
                                Return 0
                            Else
                                '�ڑ�����
                                stat = True
                                Return 0
                            End If
                        Else '���ڐ��s��v
                            '��蒼��
                        End If
                    Else '���X�|���X�s��
                        '��蒼��
                    End If

                Catch ex As Exception
                    '�^�C���A�E�g���̃G���[
                End Try

                If i = 3 Then '4�񎸔s������
                    Return 3
                End If
            Next

        Catch ex As Exception
            Return -1

        End Try
    End Function

End Module
