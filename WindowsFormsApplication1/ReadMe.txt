Fairly awful first attempt at a C# program.

The goal of it, is to flawlessly resize pre-existing .dds files after copying them, whilst keeping their directory structure intact. So far, I've got the resizing and mipmaps down. Big trick is to figure out how to get each .dds image's format, then punt that over to texconv.exe. Didn't find much in the way of the hex header of the .dds files, sadly.
