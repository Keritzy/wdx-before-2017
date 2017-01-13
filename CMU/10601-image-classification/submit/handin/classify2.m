function labels = classify2(Model,X)
    feas = [];
    for i = 1 : size(X,1)
        feas = [feas;naivehog(reshape(X(i,:),[32,32,3]))'];
    end
    
    feas = feas * Model.w;
    dist = pdist2(feas,Model.centers);
    [~,labels] = min(dist,[],2);
    labels = labels - ones(size(labels));
end
