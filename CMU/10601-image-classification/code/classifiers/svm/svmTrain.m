function [ Model ] = svmTrain( X,Y )

Model = fitcsvm(X,Y);
end

