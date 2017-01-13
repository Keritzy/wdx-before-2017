function labels = FisherClassify(Model,datas)
    datas = datas * Model.W;
    dist = pdist2(datas,Model.Centers);
    [~,labels] = min(dist,[],2);
    labels = labels - ones(size(labels));
end

