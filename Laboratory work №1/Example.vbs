Set objArgs = WScript.Arguments
Dim line
line = ""

For I = 0 to objArgs.Count - 1
    line = line + " " + objArgs(I)
Next
MsgBox(line)