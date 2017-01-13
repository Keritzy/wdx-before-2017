classdef sift < fea
    %SIFT Summary of this class goes here
    %   Detailed explanation goes here
    
    properties
        X;
        Y;
        K; % how many cluster / feature dimension
        N; % number of samples when building feature space
        visWord; %
        name;   % name of the sift model
        
        hist;   % calculate histogram or only bag of words
    end
    
    methods
        function obj = sift(K,hist)
            obj.K = K;
            obj.hist = hist;
        end
        
        function build(obj,imgs)
            [obj.X,obj.Y,obj.C,obj.N] = getImagesDim(imgs);
            
            obj.name = sprintf('siftFeaSpace_%d_%d_%d_%d_%d',obj.X,obj.Y,obj.C,obj.K,obj.N);
            
            filename = [obj.storedir,'/',obj.name];
            f = loadIfExist(filename);
            
            if f ~= -1
                obj.visWord = f.visWord;
            else
                if obj.C == 1
                    [ obj.visWord ] = siftBuild( imgs, obj.K );
                else
                    obj.visWord = zeros(128,obj.K,obj.C);
                    for i = 1 : obj.C
                        [ obj.visWord(:,:,i) ] = siftBuild( squeeze(imgs(:,:,i,:)), obj.K );
                    end
                end
            end
            
        end
        
        function fea = projection(obj,img)
            % return a row vector of feature
            if obj.hist
                fea = siftProjHist(img,obj.visWord);
            else
                fea = siftProjBin(img,obj.visWord);
            end
            fea = fea';
        end
        
        function feas= projections(obj,imgs)
            % return n rows of feature vectors
            [~,~,~,nImgs] = getImagesDim(imgs);
            feas = zeros(nImgs,obj.C * obj.K);
        end
        
        function save(obj)
        end
    end
    
end

