function [ image ] = column2Image( imageColumn )
    image(:,:,1) = reshape(imageColumn(1:1024), 32, 32);
    image(:,:,2) = reshape(imageColumn(1025:2048), 32, 32);
    image(:,:,3) = reshape(imageColumn(2049:3072), 32, 32);
end

