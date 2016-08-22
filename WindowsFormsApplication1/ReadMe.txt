Initially, the only visible facets of the program is the "Select Source Directory" panel.

Upon clicking "Source Dir", a windows explorer dialog will appear, prompting the user to navigate to a folder. Ideally, this will not show the antiquated folder dialog that organizes everything as a tree; it will function the same as the file selection dialog, but with an "Open Folder" button instead of "Open File".

Once the folder has been located, it's path will go into the text field next to the button.

The above is not absolutely necessary, as the program will use the path in the text field, rather than the path embedded in the "Source Dir" form, but it is much more user friendly. This also applies to the "Select Destination Directory" part of the program.


Once a source folder has been located, the program will make the "Resizing Conditions" panel visible. Next to each set of drop-down menus, is a checkbox. If it's ticked, the program will later use the respective "Old Resolution" and "New Resolution" values. Some Pascal-ish psuedocode:


Function DoBulkOfCode()

	if FileFromSourceDir.x == StrToInt(comboboxSourceXRes01.Text)
		if FileFromSourceDir.y == StrToInt(comboboxSourceYRes01.Text) then
			begin
				sSourceDDSFormat := GetFormat(FileFromSourceDir);
				DuplicatedFile := CopyFileIncludingFolderStructure(FileFromSourceDir, textboxDestinationDir.Text);
				texconv.exe -w StrToInt(comboboxDestinationXRes01) -h StrToInt(comboboxDestinationYRes01) -f sSourceDDSFormat DuplicatedFile;
			end;
			
end;
		
		
There are a bazillion roadblocks here. I have to somehow find every file, with the extension of .dds, from every subdirectory in the source folder, copy them whilst maintaining the folder structure & then resize the copied files. The directories would be used like so:

Using source folder "C:\BigFolder\MySource\Data"
Destination Folder is "Z:\MyResized"
Found the source file "C:\BigFolder\MySource\Data\Textures\Clothes\Male\LeatherArmor\Outfit.dds"
Copy to "Z:\MyResized\Textures\Clothes\Male\LeatherArmor\Outfit.dds"