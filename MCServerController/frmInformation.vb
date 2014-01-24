Public Class frmInformation

    Private Sub frmInformation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblAppVersion.Text = "Version " & GSTR_APP_VERSION
    End Sub

    Private Sub lnklblDevel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnklblDevel.LinkClicked
        lnklblDevel.LinkVisited = True
        System.Diagnostics.Process.Start("http://lyrical-magical.net/")
    End Sub

    Private Sub lnklblWebSite_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnklblWebSite.LinkClicked
        lnklblWebSite.LinkVisited = True
        System.Diagnostics.Process.Start(lnklblWebSite.Text)
    End Sub
End Class