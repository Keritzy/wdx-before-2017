function [Model] = train(X,Y)
    % X trainX
    % Y label
    
    feas = [];
    for i = 1 : size(X,1)
        feas = [feas;naivehog(reshape(X(i,:),[32,32,3]))'];
        i
    end
    
    Model = elmTrain(feas,Y,2000,'sig');
    save('Model.mat', 'Model');
end


