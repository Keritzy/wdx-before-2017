function [Model2] = train2(X,Y)
    feas = [];
    for i = 1 : size(X,1)
        feas = [feas;naivehog(reshape(X(i,:),[32,32,3]),8)'];
        i
    end
    Model2 = ldaTrain(feas,Y);
    save('Model2.mat','Model2');
end
    
