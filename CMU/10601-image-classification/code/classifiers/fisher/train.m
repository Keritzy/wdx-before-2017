function [Model] = train(X,Y)
    data = [];
    for i = 1 : size(X,1)
        data = [data;naivehog(reshape(X(i,:),[32,32,3]))'];
    end
    Model = trainFisher(data,Y);
    save("Model.mat", Model);
end

