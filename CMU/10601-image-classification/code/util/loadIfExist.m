function [ h ] = loadIfExist( filename )
if exist(filename, 'file')
    h = load(filename);
else
    h = -1; % return -1 if does not exist
end
end

